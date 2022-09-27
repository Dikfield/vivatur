import { ComponentFixture, TestBed } from '@angular/core/testing';

import * as promotionsListComponent from './promotions-list.component';

describe('DestinationsListComponent', () => {
  let component: promotionsListComponent.PromotionsListComponent;
  let fixture: ComponentFixture<promotionsListComponent.PromotionsListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ promotionsListComponent.PromotionsListComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(promotionsListComponent.PromotionsListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
