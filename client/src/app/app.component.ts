import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'Viva Turismo Viagens';
  destinations:any;

  constructor(private http:HttpClient) {}

  ngOnInit() {
    this.getDestinations();
  }

  getDestinations(){
    this.http.get('https://localhost:5001/api/destination').subscribe({
      next: (response) => this.destinations = response,
      error: (e) => console.log(e)
    })
  }
}
