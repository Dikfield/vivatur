import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Promotion } from 'src/app/_models/promotion';
import { PromotionsService } from 'src/app/_services/promotions.service';

@Component({
  selector: 'app-register-promotions',
  templateUrl: './register-promotions.component.html',
  styleUrls: ['./register-promotions.component.css']
})
export class RegisterPromotionsComponent implements OnInit {
  model:any = {};
  @Output() promotionRegistered = new EventEmitter();

  constructor(public promotionService:PromotionsService,
    private toastr:ToastrService, private router:Router) { }

  ngOnInit(): void {
  }

  register(){
    this.promotionService.registerPromotion(this.model).subscribe({
      next:(promotions:Promotion)=>{
        this.toastr.success("Registered");
        this.promotionRegistered.emit();
        this.router.navigateByUrl('promotion/edit/' + promotions.id);
      }, error:(e)=> {
        console.log(e);
        this.toastr.error(e.error)}
    })
  }


  cancel(){
    console.log('cancelled');
  }

}
