import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Destination } from 'src/app/_models/destination';
import { DestinationsService } from 'src/app/_services/destinations.service';

@Component({
  selector: 'app-destinations-list',
  templateUrl: './destinations-list.component.html',
  styleUrls: ['./destinations-list.component.css']
})
export class DestinationsListComponent implements OnInit {
    destinations$: Observable<Destination[]>;

    constructor(private destinationService:DestinationsService) { }

  ngOnInit(): void {
    this.destinations$ = this.destinationService.getDestinations();
  }

}
