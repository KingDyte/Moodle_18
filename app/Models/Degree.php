<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Collection;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

/**
 * @property int $id
 * @property string $name
 * @property Collection<Course> $courses
 */
class Degree extends Model
{
    public $timestamps = false;

    public $table = 'degrees';

    protected $fillable = [
        'name',
    ];

    public function courses(): BelongsToMany
    {
        return $this->belongsToMany(Course::class, 'approved_degrees', 'degree_id', 'course_id');
    }
}
