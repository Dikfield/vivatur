import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Promotion } from 'src/app/_models/promotion';
import { PromotionsService } from 'src/app/_services/promotions.service';

@Component({
  selector: 'app-promotions-list',
  templateUrl: './promotions-list.component.html',
  styleUrls: ['./promotions-list.component.css']
})
export class PromotionsListComponent implements OnInit {
    promotions: Promotion[];
    @Input() promotion:Promotion;

    constructor(private promotionService:PromotionsService, private router:Router,
      private toastr:ToastrService) { }

  ngOnInit(): void {
    this.initialState();
  }

  promotionDeleted(id:number) {
    this.promotions = this.promotions.filter(
      promotion => promotion.id != id
    );
  }

    initialState() {
      this.promotionService.getPromotions().subscribe({
        next:(promotions) =>{
          this.promotions = promotions;
        }
      });
    }

  }




