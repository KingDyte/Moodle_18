CREATE TABLE `users` (
  `id` integer PRIMARY KEY,
  `username` varchar(255),
  `name` varchar(255),
  `password` varchar(255),
  `degree_id` integer,
  FOREIGN KEY (`degree_id`) REFERENCES `degrees` (`id`)
);

CREATE TABLE `mycourses` (
  `id` integer PRIMARY KEY,
  `user_id` integer,
  `course_id` integer,
  FOREIGN KEY (`user_id`) REFERENCES `users` (`id`),
  FOREIGN KEY (`course_id`) REFERENCES `courses` (`id`)
);

CREATE TABLE `courses` (
  `id` integer PRIMARY KEY,
  `code` varchar(255),
  `name` varchar(255),
  `credit` integer
);

CREATE TABLE `degrees` (
  `id` integer PRIMARY KEY,
  `name` varchar(255)
);

CREATE TABLE `approved_degress` (
  `id` integer PRIMARY KEY,
  `course_id` integer,
  `degree_id` integer,
  FOREIGN KEY (`degree_id`) REFERENCES `degrees` (`id`),
  FOREIGN KEY (`course_id`) REFERENCES `courses` (`id`)
);

CREATE TABLE `events` (
  `id` integer PRIMARY KEY,
  `course_id` integer,
  `name` varchar(255),
  `description` varchar(255),
  FOREIGN KEY (`course_id`) REFERENCES `courses` (`id`)
);


