<?php

use App\Http\Controllers\CourseController;
use Illuminate\Support\Facades\Route;
use Inertia\Inertia;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return redirect('/login');
});

Route::get('/dashboard', function () {
    return Inertia::render('Dashboard', [
        'loggedUser' => [
            'id' => auth()->user()->id,
            'degreeId' => auth()->user()->degree_id,
            'courses' => array_map(fn ($course) => $course['id'],  auth()->user()->courses->toArray())
        ]
    ]);
})->middleware(['auth', 'verified'])->name('dashboard');

Route::middleware('auth')->group(function () {
    Route::get('/courses/list', [CourseController::class, 'courses'])->name('courses.list');
    Route::post('/courses/apply', [CourseController::class, 'apply'])->name('courses.apply');
    Route::get('/courses/users/{courseId}', [CourseController::class, 'users'])->name('courses.users');
});

require __DIR__.'/auth.php';
