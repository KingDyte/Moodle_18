<?php

namespace App\Models;

use Entities\NxApplicationUser;
use Illuminate\Database\Eloquent\Collection;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

/**
 * @property int $id
 * @property string $code
 * @property string $name
 * @property int $credit
 * @property Collection<Degree> $degrees
 * @property Collection<User> $users
 */
class Course extends Model
{
    public $timestamps = false;

    public $table = 'courses';

    protected $fillable = [
        'code',
        'name',
        'credit'
    ];

    public function degrees(): BelongsToMany
    {
        return $this->belongsToMany(Degree::class, 'approved_degrees', 'course_id', 'degree_id');
    }

    public function users(): BelongsToMany
    {
        return $this->belongsToMany(User::class, 'mycourses', 'course_id', 'user_id');
    }

    public function isAllow(User $user): bool
    {
        return
            in_array(
                $user->degree_id,
                array_map(fn ($degree) => $degree['id'], $this->degrees->toArray())
            )
            &&
            !in_array(
                $this->id,
                array_map(fn ($course) => $course['id'], $user->courses->toArray())
            );
    }
}
