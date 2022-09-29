import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PromotionsService } from 'src/app/_services/promotions.service';

@Component({
  selector: 'app-register-promotions',
  templateUrl: './register-promotions.component.html',
  styleUrls: ['./register-promotions.component.css']
})
export class RegisterPromotionsComponent implements OnInit {
  model:any = {};

  constructor(public promotionService:PromotionsService,
    private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
  }

  register(){
    this.promotionService.registerPromotion(this.model).subscribe({
      next:(response)=>{
        this.toastr.success("Registered");
        this.router.navigateByUrl('promotions');
      }, error:(e)=> {
        console.log(e);
        this.toastr.error(e.error)}
    })
  }


  cancel(){
    console.log('cancelled');
  }

}
