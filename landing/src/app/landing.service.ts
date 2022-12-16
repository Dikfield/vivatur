import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { BehaviorSubject, map, of } from 'rxjs';
import { About } from './_models/about';
import { Destination } from './_models/destination';
import { Promotion } from './_models/promotion';
import { Feedback } from './_models/feedback';

@Injectable({
  providedIn: 'root'
})
export class LandingService{
  baseUrl = environment.apiUrl;
  destinations: Destination [] = [];
  dest = new BehaviorSubject(this.destinations);
  sharedDestinations = this.dest.asObservable();
  abouts:About[] = [];
  feedbacks:Feedback[] = [];
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

getFeedbacks() {
  if(this.feedbacks.length > 0) return of(this.feedbacks);
  return this.http.get<Feedback[]>(this.baseUrl + 'about/feed').pipe(
    map(feedbacks =>{
      this.feedbacks = feedbacks;
      return feedbacks;
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
