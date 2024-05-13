<?php

namespace Tests\Feature;

use App\Models\Folder;
use App\Models\User;
use Illuminate\Foundation\Testing\RefreshDatabase;
use Illuminate\Foundation\Testing\WithFaker;
use Tests\TestCase;

class FolderTest extends TestCase
{
    use RefreshDatabase;

    protected static bool $migrated = false;

    public function setUp(): void
    {
        parent::setUp();

        if (!self::$migrated) {
            $this->refreshDatabaseOnce();
            self::$migrated = true;
        }
    }

    private function refreshDatabaseOnce(): void
    {
        $this->artisan('migrate:refresh');
        $this->artisan('db:seed');
    }

    private function getUser(): User
    {
        return User::where('email', 'john@doe.com')->first();
    }

    private function getExistFolder(int $userId, int $folderId = null): ?Folder
    {
        return Folder::where('user_id', $userId)
            ->where('parent_folder_id', $folderId)->first();
    }

    /**
     * @group getParentFolders
     *
     * @return void
     */
    public function testGetParentFolders(): void
    {
        $response = $this->actingAs($this->getUser())->get(route('folder.get-parent-folders'));
        $response->assertStatus(200);
        $response->assertJsonStructure(['folders' => []]);
        $this->assertCount(0, $response->original['folders']);
    }

    /**
     * @group createFolderWithEmptyPost
     * @group createFolder
     *
     * @return void
     */
    public function testCreateFolderWithEmptyPost(): void
    {
        $response = $this->actingAs($this->getUser())->post(route('folder.create'), []);
        $response->assertStatus(200);
        $response->assertJsonStructure(['errors' => []]);
        $this->assertNotEmpty($response->original['errors']);
    }

    /**
     * @group createFolderFromRoot
     * @group createFolder
     *
     * @return void
     */
    public function testCreateFolderFromRoot(): void
    {
        $post = [
            'name' => 'First folder',
            'parentFolderId' => null
        ];
        $response = $this->actingAs($this->getUser())->post(route('folder.create'), $post);
        $response->assertStatus(200);
        $response->assertJsonStructure(['success']);
        $this->assertTrue($response->original['success']);
    }

    /**
     * @group createFolderWithExistName
     * @group createFolder
     *
     * @return void
     */
    public function testCreateFolderWithExistName(): void
    {
        $this->testCreateFolderFromRoot();

        $post = [
            'name' => 'First folder',
            'parentFolderId' => null
        ];
        $response = $this->actingAs($this->getUser())->post(route('folder.create'), $post);
        $response->assertStatus(200);
        $response->assertJsonStructure(['errors' => []]);
        $this->assertTrue(gettype($response->original['errors']) === 'object');
        foreach ($response->original['errors']->toArray() as $error) {
            $this->assertEquals('The name has already been taken.', $error[0]);
        }
    }

    /**
     * @group updateFolder
     *
     * @return void
     * @throws \Exception
     */
    public function testUpdateFolder(): void
    {
        $this->testCreateFolderFromRoot();

        $user = $this->getUser();
        $folder = $this->getExistFolder($user->id);
        if(is_null($folder)) {
            throw new \Exception('Folder not found!');
        }

        $put['folder'] = [
            'id' => $folder->id,
            'name' => $folder->name . ' edit',
            'parentFolderId' => $folder->parent_folder_id
        ];
        $response = $this->actingAs($user)->put(route('folder.update-folder'), $put);
        $response->assertStatus(200);
        $response->assertJsonStructure(['success', 'affected']);
        $this->assertTrue($response->original['success']);
        $this->assertEquals(1, $response->original['affected']);
    }

    /**
     * @group createNestedFolders
     * @group createFolder
     * @return void
     */
    public function testCreateNestedFolders(): void
    {
        $this->testCreateFolderFromRoot();
        $user = $this->getUser();
        $folder = $this->getExistFolder($user->id);

        $post = [
            'name' => 'Sub folder',
            'parentFolderId' => $folder->id
        ];
        $response = $this->actingAs($this->getUser())->post(route('folder.create'), $post);
        $response->assertStatus(200);
        $response->assertJsonStructure(['success']);
        $this->assertTrue($response->original['success']);
    }

    /**
     * @group deleteFolder
     *
     * @return void
     * @throws \Exception
     */
    public function testDeleteFolder(): void
    {
        $this->testCreateFolderFromRoot();
        $user = $this->getUser();
        $folder = $this->getExistFolder($user->id);
        if(is_null($folder)) {
            throw new \Exception('Folder not found!');
        }

        $delete = ['folderId' => $folder->id];
        $response = $this->actingAs($user)->delete(route('folder.delete-folder'), $delete);
        $response->assertStatus(200);
        $response->assertJsonStructure(['success']);
        $this->assertTrue($response->original['success']);
    }

    /**
     * @group deleteNestedFolders
     * @group deleteFolder
     * @return void
     */
    public function testDeleteNestedFolders(): void
    {
        $this->testCreateNestedFolders();
        $user = $this->getUser();
        $this->assertCount(2, Folder::where('user_id', $user->id)->get());
        $folder = $this->getExistFolder($user->id);

        $delete = ['folderId' => $folder->id];
        $response = $this->actingAs($user)->delete(route('folder.delete-folder'), $delete);
        $response->assertStatus(200);
        $response->assertJsonStructure(['success']);
        $this->assertTrue($response->original['success']);
        $this->assertCount(0, Folder::where('user_id', $user->id)->get());
    }
}
