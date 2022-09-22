import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Destination } from 'src/app/_models/destination';
import { DestinationPhoto } from 'src/app/_models/destinationPhoto';
import { DestinationsService } from 'src/app/_services/destinations.service';

@Component({
  selector: 'app-destination-edit',
  templateUrl: './destination-edit.component.html',
  styleUrls: ['./destination-edit.component.css']
})
export class DestinationEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  destination:Destination;

  @HostListener('window:beforeunload', ['$event']) unloadNotification($event:any){
    if(this.editForm.dirty){
      $event.returnValue = true;
    }
  }

  constructor(private destinationService:DestinationsService, private route: ActivatedRoute,
    private toastr:ToastrService) {

   }

  ngOnInit(): void {
    this.loadDestination();

  }

  loadDestination() {
    this.destinationService.getDestination(this.route.snapshot.paramMap.get('name')).subscribe({
      next:(destination) => this.destination = destination
    })
  }
  updateDestination(){
    this.destinationService.updateDestination(this.destination).subscribe({
      next:() => {this.toastr.success('Destino atualizado');
      this.editForm.reset(this.destination);
    }
    })
  }

  updateDescription (model) {
    console.log(model)
  }
}
