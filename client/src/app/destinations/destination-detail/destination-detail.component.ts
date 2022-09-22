import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Destination } from 'src/app/_models/destination';
import { DestinationsService } from 'src/app/_services/destinations.service';


@Component({
  selector: 'app-destination-detail',
  templateUrl: './destination-detail.component.html',
  styleUrls: ['./destination-detail.component.css']
})
export class DestinationDetailComponent implements OnInit {
  destination:Destination;

  constructor(private destinationService:DestinationsService,
    private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadDestination();

  }

  loadDestination() {
    this.destinationService.getDestination(this.route.snapshot.paramMap.get('name')).subscribe({
      next:(destination) => this.destination = destination
    })
  }

}
