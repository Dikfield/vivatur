import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register-destinations',
  templateUrl: './register-destinations.component.html',
  styleUrls: ['./register-destinations.component.css']
})
export class RegisterDestinationsComponent implements OnInit {
  model:any = {};

  constructor(public accountService:AccountService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  register(){
    this.accountService.register(this.model).subscribe({
      next:(response)=>{
        console.log(response);
        this.toastr.success();
      }, error:(e)=> {
        console.log(e);
        this.toastr.error(e.error)}
    })
  }

  cancel(){
    console.log('cancelled');
  }

}
