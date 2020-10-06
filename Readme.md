#Instruction
 1. I have use .net core 2.1 wep api application
 2. I have use mongodb as Database source
 2. I have use jwt tokken based authentication you can find out Jwtmid class in project that is resposible for checking tokken and assign itno current context.
 3. I have single class for all user and score data.
 4. I have use repository 
 

Please check below are the document of how to use project apis . 

 
Step1. You need to Generate Secure Tokken (Note:Currenlty it is not varified by db)
  GET : https://localhost:44363/generateTokken/sachin  
  Output :eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InNhY2hpbiIsIm5iZiI6MTYwMTA5OTQ1NSwiZXhwIjoxNjAxNzA0MjU1LCJpYXQiOjE2MDEwOTk0NTV9.IHIJDGn-GoaXMXpjf_H8eyymucBQA8Ye8R9YYajzEy8
  
Step2. Add New Score Record								
  POST: https://localhost:44363/addscore   
  Header:  Authorization:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InNhY2hpbiIsIm5iZiI6MTYwMTA5OTQ1NSwiZXhwIjoxNjAxNzA0MjU1LCJpYXQiOjE2MDEwOTk0NTV9.IHIJDGn-GoaXMXpjf_H8eyymucBQA8Ye8R9YYajzEy8
  Payload : 
	  { 
		  "UserName":"sachin", 
		  "MatchName":"pubg",
		  "Rank":1,
		  "Kills":22,
		  "Score":12,
	  }
  
  Output:
	{"Id":5f7c23f8966b901508689390,"UserName":"sachin","MatchName":"pubg","Rank":"1","Kills":"22","Score":"120","EnteryDate":"2020-10-06T13:29:49.519+05:30"}
  
Step3. Fetching LeaderBoad         
 
   GET: https://localhost:44363/LeaderBoad?matchname=pubg&time=daily
   Header:  Authorization:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InNhY2hpbiIsIm5iZiI6MTYwMTA5OTQ1NSwiZXhwIjoxNjAxNzA0MjU1LCJpYXQiOjE2MDEwOTk0NTV9.IHIJDGn-GoaXMXpjf_H8eyymucBQA8Ye8R9YYajzEy8
   
  Output: 
  {
      "stats":[
		{"Id": "5f7c23f8966b901508689390", "UserName": "sachin", "Rank": "1", "Kills": "1",�},
		{"Id": "5f7cafb81d5dcaf2d8e71900", "UserName": "rakesh", "Rank": "2", "Kills": "12",�},
		{"Id": "5f7cafcd1d5dcaf2d8e71901", "UserName": "rakesh", "Rank": "2", "Kills": "12",�}
	   ]
  }

Step4. Fetching PlayerStats
  
  GET: https://localhost:44363/playersStats/sachin 
  Header:  Authorization:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InNhY2hpbiIsIm5iZiI6MTYwMTA5OTQ1NSwiZXhwIjoxNjAxNzA0MjU1LCJpYXQiOjE2MDEwOTk0NTV9.IHIJDGn-GoaXMXpjf_H8eyymucBQA8Ye8R9YYajzEy8
   
  Output:
  {
    "PlayersStats":[
	{
		"Id": "5f7c23f8966b901508689390",
		"MatchName": "pubg",
		"Kills": "1",
		"Score": "1",
		"EnteryDate": "2020-10-06T13:29:49.519+05:30"
    }
  ]
}
  
Step5.Fetching PlayerStats with filter
  
  GET: https://localhost:44363/playersStats?matchname=pubg&time=daily 
   Header:  Authorization:eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6InNhY2hpbiIsIm5iZiI6MTYwMTA5OTQ1NSwiZXhwIjoxNjAxNzA0MjU1LCJpYXQiOjE2MDEwOTk0NTV9.IHIJDGn-GoaXMXpjf_H8eyymucBQA8Ye8R9YYajzEy8
   
  Output:
  {
	"PlayersStats":[
		{"Id": "5f7c23f8966b901508689390", "UserName": "sachin", "Kills": "1", "Score": "1",�},
		{"Id": "5f7cafb81d5dcaf2d8e71900", "UserName": "rakesh", "Kills": "12", "Score": "10",�},
		{"Id": "5f7cafcd1d5dcaf2d8e71901", "UserName": "rakesh", "Kills": "12", "Score": "10",�}
    ]
  }

  