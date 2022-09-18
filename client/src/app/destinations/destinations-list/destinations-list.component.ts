import { Component, OnInit } from '@angular/core';
import { Destination } from 'src/app/_models/destination';
import { DestinationsService } from 'src/app/_services/destinations.service';

@Component({
  selector: 'app-destinations-list',
  templateUrl: './destinations-list.component.html',
  styleUrls: ['./destinations-list.component.css']
})
export class DestinationsListComponent implements OnInit {
    destinations: Destination[];

    constructor(private destinationService:DestinationsService) { }

  ngOnInit(): void {
    this.loadDestinations();
  }

  loadDestinations() {
    this.destinationService.getDestinations().subscribe({
      next:(destinations) =>
        this.destinations = destinations
      })
    }

}
