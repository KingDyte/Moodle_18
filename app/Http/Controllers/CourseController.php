<?php

namespace App\Http\Controllers;

use App\Http\Requests\ApplyRequest;
use App\Models\Course;
use App\Models\Degree;
use Illuminate\Database\Eloquent\Builder;
use Illuminate\Http\JsonResponse;
use Illuminate\Http\Request;

class CourseController extends Controller
{
    /**
     * Kurzusok listája
     * @param Request $request
     * @return JsonResponse
     */
    public function courses(Request $request): JsonResponse
    {
        $qb = Course::with('degrees')
            ->orderBy('name');
        if ($request->has('degreeId')) {
            $qb->whereHas('degrees', function (Builder $query) use ($request) {
                $query->where('degrees.id', '=', (int)$request->get('degreeId'));
            });
        }
        if ($request->has('isMy') && (bool)$request->get('isMy')) {
            $qb->whereHas('users', function (Builder $query) use ($request) {
                $query->where('users.id', '=', auth()->user()->id);
            });
        }

        return response()->json([
            'courses' => $qb->get(),
            'degrees' => Degree::orderBy('name')->pluck('name', 'id'),
            'existsCourses' => Course::whereHas('users', function (Builder $query) {
                    $query->where('mycourses.user_id', '=', auth()->user()->id);
                })
                ->pluck('id')
        ]);
    }

    /**
     * Jelentkezés a kurzusra
     * @param ApplyRequest $request
     * @return JsonResponse|\Illuminate\Http\Response
     */
    public function apply(ApplyRequest $request)
    {
        $course = Course::find($request->post('courseId'));
        if ($course instanceof Course && $course->isAllow(auth()->user())) {
            $course->users()->save(auth()->user());
            return response()->json(['success' => true]);
        }
        return response('Bad request', 400);
    }

    /**
     * Kurzushoz tartozó felhasználók
     * @param int $courseId
     * @return JsonResponse
     */
    public function users(int $courseId): JsonResponse
    {
        $course = Course::find($courseId);
        return response()->json([
            'course' => $course,
            'users' => $course->users,
        ]);
    }
}
