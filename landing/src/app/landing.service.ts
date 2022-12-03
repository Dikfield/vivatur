import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { map, of } from 'rxjs';
import { Destination } from './_models/destination';

@Injectable({
  providedIn: 'root'
})
export class LandingService {
  baseUrl = environment.apiUrl;
  destinations: Destination [] = [];

  constructor(private http:HttpClient) { }


  getDestinations() {
    if(this.destinations.length > 0) return of(this.destinations);
   return this.http.get<Destination[]>(this.baseUrl + 'destination').pipe(
     map(destinations =>{
       this.destinations = destinations;
       return destinations;
     })
   )
 }
}
