import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Destination } from 'src/app/_models/destination';
import { DestinationsService } from 'src/app/_services//destinations.service';

@Component({
  selector: 'app-register-destinations',
  templateUrl: './register-destinations.component.html',
  styleUrls: ['./register-destinations.component.css']
})
export class RegisterDestinationsComponent implements OnInit {
  model:any = {};
  check:boolean = true;
  @Output() destinationRegistered = new EventEmitter();

  constructor(public destinationService:DestinationsService,
    private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
  }

  register(){
    this.destinationService.registerDestination(this.model).subscribe({
      next:(destination:Destination)=>{
        this.toastr.success("Registered");
        this.destinationRegistered.emit();
        this.router.navigateByUrl('destination/edit/' + destination.id);

      }, error:(e)=> {
        console.log(e);
        this.toastr.error(e.error)}
    })
  }


  cancel(){
    console.log('cancelled');
  }

}
