import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { filter, Observable } from 'rxjs';
import { Destination } from 'src/app/_models/destination';
import { DestinationsService } from 'src/app/_services/destinations.service';

@Component({
  selector: 'app-destinations-list',
  templateUrl: './destinations-list.component.html',
  styleUrls: ['./destinations-list.component.css']
})
export class DestinationsListComponent implements OnInit {
    destinations: Destination[];
    @Input() destination:Destination;

    constructor(private destinationService:DestinationsService, private router:Router,
      private toastr:ToastrService) { }

  ngOnInit(): void {
    this.initialState();
  }

  destinationDeleted(id:number) {
    this.destinations = this.destinations.filter(
      destination => destination.id != id
    );

  }

  initialState() {
    this.destinationService.getDestinations().subscribe({
      next:(destinations) =>{
        this.destinations = destinations;
      }
    });
  }

}
