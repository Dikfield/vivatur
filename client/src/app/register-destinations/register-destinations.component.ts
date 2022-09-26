import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';
import { DestinationsService } from '../_services/destinations.service';

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
      next:(response)=>{
        this.reloadCurrentRoute();
        this.toastr.success("Registered");
      }, error:(e)=> {
        console.log(e);
        this.toastr.error(e.error)}
    })
  }

  reloadCurrentRoute() {
    let currentUrl = this.router.url;
    this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
        this.router.navigate([currentUrl]);
    });
}

  cancel(){
    console.log('cancelled');
  }

}
