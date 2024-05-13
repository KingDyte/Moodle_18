<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasOne;

/**
 * @property int $id
 * @property int $course_id
 * @property string $name
 * @property string $description
 * @property Course $course
 */
class Event extends Model
{
    public $timestamps = false;

    public $table = 'events';

    protected $fillable = [
        'course_id',
        'name',
        'description'
    ];

    public function course(): BelongsTo
    {
        return $this->belongsTo(Course::class);
    }
}
