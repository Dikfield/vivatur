import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { map, of } from 'rxjs';
import { About } from './_models/about';
import { Destination } from './_models/destination';
import { Promotion } from './_models/promotion';

@Injectable({
  providedIn: 'root'
})
export class LandingService {
  baseUrl = environment.apiUrl;
  destinations: Destination [] = [];
  abouts:About[] = [];
  promotions: Promotion [] = [];

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

 getAbouts() {
  if(this.abouts.length > 0) return of(this.abouts);
  return this.http.get<About[]>(this.baseUrl + 'about').pipe(
    map(abouts =>{
      this.abouts = abouts;
      return abouts;
    })
  )
}

getPromotions() {
  if(this.promotions.length > 0) return of(this.promotions);
  return this.http.get<Promotion[]>(this.baseUrl + 'promotion').pipe(
    map(promotions =>{
      this.promotions = promotions;
      return promotions;
    })
  )
}
}
