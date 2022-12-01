import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
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
    modalRef?:BsModalRef;

    constructor(private promotionService:PromotionsService, private router:Router,
      private toastr:ToastrService, private modalService: BsModalService) { }

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




