﻿INSERT INTO Users ( username, name, password) VALUES
( 'user1', 'User One', 'password1'),
( 'user2', 'User Two', 'password2'),
( 'user3', 'User Three', 'password3'),
( 'user4', 'User Four', 'password4');

INSERT INTO Ttypes ( name) VALUES
( 'Type One'),
( 'Type Two'),
( 'Type Three');

INSERT INTO Topics ( name, TypeId, description) VALUES
( 'Topic 1', 1, 'Description for Topic 1'),
( 'Topic 2', 2, 'Description for Topic 2'),
( 'Topic 3', 1, 'Description for Topic 3'),
( 'Topic 4', 3, 'Description for Topic 4');

insert into favtopics (userid, topicid) values
(1, 1),
(1, 2),
(2, 1),
(3, 3),
(4, 4);

INSERT INTO Comments ( UserId, TopicId, body, timestamp) VALUES
( 1, 1, 'Comment 1 for Topic 1', '2024-04-01 10:00:00'),
( 1, 1, 'Comment 2 for Topic 1', '2024-04-01 10:30:00'),
( 2, 2, 'Comment 1 for Topic 2', '2024-04-01 11:00:00'),
( 3, 3, 'Comment 1 for Topic 3', '2024-04-01 11:30:00'),
( 4, 4, 'Comment 1 for Topic 4', '2024-04-01 12:00:00');