<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('degrees', function (Blueprint $table) {
            $table->increments('id');
            $table->string('name');
        });

        Schema::create('courses', function (Blueprint $table) {
            $table->increments('id');
            $table->string('code');
            $table->string('name');
            $table->integer('credit', unsigned: true);
        });

        Schema::create('approved_degrees', function (Blueprint $table) {
            $table->increments('id');
            $table->integer('course_id', unsigned: true);
            $table->integer('degree_id', unsigned: true);
        });

        Schema::table('approved_degrees', function (Blueprint $table) {
            $table->foreign('course_id')->references('id')->on('courses');
            $table->foreign('degree_id')->references('id')->on('degrees');
        });

        Schema::create('users', function (Blueprint $table) {
            $table->increments('id');
            $table->string('name');
            $table->string('email')->unique();
            $table->timestamp('email_verified_at')->nullable();
            $table->string('password');
            $table->integer('degree_id', unsigned: true);
            $table->rememberToken();
            $table->timestamps();
        });

        Schema::table('users', function (Blueprint $table) {
            $table->foreign('degree_id')->references('id')->on('degrees');
        });

        Schema::create('mycourses', function (Blueprint $table) {
            $table->increments('id');
            $table->integer('user_id', unsigned: true);
            $table->integer('course_id', unsigned: true);
        });

        Schema::table('mycourses', function (Blueprint $table) {
            $table->foreign('user_id')->references('id')->on('users');
            $table->foreign('course_id')->references('id')->on('courses');
        });

        Schema::create('events', function (Blueprint $table) {
            $table->increments('id');
            $table->integer('course_id', unsigned: true);
            $table->string('name');
            $table->string('description');
        });

        Schema::table('events', function (Blueprint $table) {
            $table->foreign('course_id')->references('id')->on('courses');
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('events');
        Schema::dropIfExists('mycourses');
        Schema::dropIfExists('users');
        Schema::dropIfExists('approved_degrees');
        Schema::dropIfExists('courses');
        Schema::dropIfExists('degrees');
    }
};
