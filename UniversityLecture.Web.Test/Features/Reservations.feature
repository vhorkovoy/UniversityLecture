Feature: Reservations
	Checking if functionality of managing (create, delete) 
	reservations is working as expected

Scenario: Create reservation
Given I am a user
And I make a post request to 'api/Account' to get token with the following data 
| Login | Password |
| demo  | demo     |
And the response status code is '200'
And the response data should be '*'
When I make a post request to 'api/reservations' with the following data 
| LectureHallId | LecturerId | Date       | StartAt | EndAt |
| 1             | 1          | 05.12.2019 | 08:00   | 11:00 |
Then the response status code is '201'
And the response data should be '{"id":*}'
And I make delete request 'api/reservations' to delete created reservation