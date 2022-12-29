import { Component, EventEmitter, Input, OnInit, Output, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService, ModalDirective } from 'ngx-bootstrap/modal';
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
    modalRef?:BsModalRef;

    constructor(private destinationService:DestinationsService, private router:Router,
      private toastr:ToastrService, private modalService: BsModalService) { }

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

  openModal(template:TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  closeModal() {
    if (!this.modalRef) {
      return;
    }

    this.modalRef.hide();
    this.modalRef = null;
  }


}
