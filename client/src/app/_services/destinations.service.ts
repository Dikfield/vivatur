import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Destination } from '../_models/destination';
import { DestinationDescription } from '../_models/destinationDescription';


@Injectable({
  providedIn: 'root'
})
export class DestinationsService {
  baseUrl = environment.apiUrl;
  destinations: Destination [] = [];
  descriptions:DestinationDescription [] = []

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

  getDestination(name:string) {
    const destination = this.destinations.find(x => x.name === name);
    if(destination !== undefined) return of(destination);
    return this.http.get<Destination>(this.baseUrl + 'destination/' + name);
  }

  updateDestination(destination:Destination){
    return this.http.put(this.baseUrl + 'destination/' + destination.name, destination).pipe(
      map(() => {
        const index = this.destinations.indexOf(destination);
        this.destinations[index] = destination;
      })
    )
  }

  updateDescription(description:DestinationDescription){
    return this.http.put(this.baseUrl + 'destination/description/update/' + description.id, description).pipe(
      map(() => {
        const index = this.descriptions.indexOf(description);
        this.descriptions[index] = description;
      })
    )
  }

  setMainPhoto(name:string, photoId:number) {
    return this.http.put(this.baseUrl + 'destination/set-main-photo/'+ name +'/' + photoId, {})
  }

  deletePhoto(photoId:number){
    return this.http.delete(this.baseUrl + 'destination/delete-photo/' + photoId);
  }
}
