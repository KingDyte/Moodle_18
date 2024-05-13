<?php

namespace App\Events;

use App\Models\Event;
use Illuminate\Broadcasting\InteractsWithSockets;
use Illuminate\Broadcasting\PrivateChannel;
use Illuminate\Contracts\Broadcasting\ShouldBroadcast;
use Illuminate\Foundation\Events\Dispatchable;
use Illuminate\Queue\SerializesModels;

class Course implements ShouldBroadcast
{
    use Dispatchable, InteractsWithSockets, SerializesModels;

    public string $title;
    public string $content;
    private Event $event;

    public function __construct(Event $event)
    {
        $this->event = $event;
        $this->title = $event->course->name.': '.$event->name;
        $this->content = $event->description;
    }

    public function broadcastOn()
    {
        return new PrivateChannel('course.'.$this->event->course_id);
    }
}
