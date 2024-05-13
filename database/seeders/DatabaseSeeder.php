<?php

namespace Database\Seeders;

use App\Models\User;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Hash;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
        for ($i=1; $i<6; $i++) {
            DB::table('degrees')->insert([
                'id' => $i,
                'name' => 'Szak '.$i,
            ]);
        }

        DB::table('users')->insert([
            'name' => 'Diak1',
            'email' => 'diak1@example.com',
            'password' => Hash::make('12345'),
            'degree_id' => 4,
        ]);

        DB::table('users')->insert([
            'name' => 'Diak2',
            'email' => 'diak2@example.com',
            'password' => Hash::make('12345'),
            'degree_id' => 3,
        ]);

        for ($i=1; $i<50; $i++) {
            DB::table('courses')->insert([
                'code' => 'C'.$i,
                'name' => 'Kurzus '.$i,
                'credit' => rand(1,5),
            ]);
        }

        $courses = DB::table('courses')->pluck('id');
        foreach($courses as $id) {
            DB::table('approved_degrees')->insert([
                'course_id' => $id,
                'degree_id' => rand(1,3)
            ]);
            DB::table('approved_degrees')->insert([
                'course_id' => $id,
                'degree_id' => rand(4,5)
            ]);
        }

        $userIds = DB::table('users')->pluck('id', 'degree_id');
        $existsCourses = [];
        foreach($userIds as $degreeId => $id) {
            $coursesIDs = DB::table('approved_degrees')->where('degree_id', '=', $degreeId)->pluck('course_id');

            $i = 0;
            foreach ($coursesIDs as $courseId) {
                DB::table('mycourses')->insert([
                    'course_id' => $courseId,
                    'user_id' => $id
                ]);
                $existsCourses[] = $courseId;
                if ($i++ > 6) {
                    break;
                }
            }
        }

        foreach ($existsCourses as $i => $courseId) {
            DB::table('events')->insert([
                'course_id' => $courseId,
                'name' => 'Esemény '.$i,
                'description' => 'Esemény leírás'.$i,
            ]);
        }
    }
}
