import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Destination } from 'src/app/_models/destination';
import { DestinationDescription } from 'src/app/_models/destinationDescription';
import { DestinationsService } from 'src/app/_services/destinations.service';

@Component({
  selector: 'app-description',
  templateUrl: './description.component.html',
  styleUrls: ['./description.component.css']
})
export class DescriptionComponent implements OnInit {
  @Input() destination:Destination;
  @ViewChild('dataForm') dataForm: NgForm;
  newDescription:boolean = false;


  constructor(private destinationService:DestinationsService, private toastr:ToastrService) {

  }

  ngOnInit(): void {
  }

  addNewDescription(){
    this.newDescription = true;
  }

  updateDescription(description:DestinationDescription){
    this.destinationService.updateDescription(description).subscribe({
      next:() => {this.toastr.success('Destino atualizado');
      this.dataForm.reset(description);
    }
    })
  }

}
