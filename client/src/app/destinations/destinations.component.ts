import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-destinations',
  templateUrl: './destinations.component.html',
  styleUrls: ['./destinations.component.css']
})
export class DestinationsComponent implements OnInit {
  Destinations: any;

  constructor(private http:HttpClient) { }

  ngOnInit(): void {
  }

  getDestinations() {
    this.http.get('https://localhost:5001/api/destination').subscribe(Destinations => this.Destinations = Destinations);
  }

}
