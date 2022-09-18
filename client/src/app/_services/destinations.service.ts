import { HttpClient} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Destination } from '../_models/destination';


@Injectable({
  providedIn: 'root'
})
export class DestinationsService {
  baseUrl = environment.apiUrl;

  constructor(private http:HttpClient) { }

  getDestinations() {
    return this.http.get<Destination[]>(this.baseUrl + 'destination');
  }

  getDestination(name:string) {
    return this.http.get<Destination>(this.baseUrl + 'destination/' + name);
  }
}
