import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register-destinations',
  templateUrl: './register-destinations.component.html',
  styleUrls: ['./register-destinations.component.css']
})
export class RegisterDestinationsComponent implements OnInit {
  model:any = {};

  constructor(public accountService:AccountService) { }

  ngOnInit(): void {
  }

  register(){
    console.log(this.model);
  }

  cancel(){
    console.log('cancelled');
  }

}
