import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DestinationsService } from 'src/app/_services//destinations.service';

@Component({
  selector: 'app-register-destinations',
  templateUrl: './register-destinations.component.html',
  styleUrls: ['./register-destinations.component.css']
})
export class RegisterDestinationsComponent implements OnInit {
  model:any = {};

  constructor(public destinationService:DestinationsService,
    private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
  }

  register(){
    this.destinationService.registerDestination(this.model).subscribe({
      next:()=>{
        this.toastr.success("Registered");
        this.router.navigateByUrl('destinations');

      }, error:(e)=> {
        console.log(e);
        this.toastr.error(e.error)}
    })
  }



  cancel(){
    console.log('cancelled');
  }

}
