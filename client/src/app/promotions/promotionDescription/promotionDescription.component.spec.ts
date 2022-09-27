import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PromotionDescriptionComponent } from './promotionDescription.component';

describe('DescriptionComponent', () => {
  let component: PromotionDescriptionComponent;
  let fixture: ComponentFixture<PromotionDescriptionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PromotionDescriptionComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PromotionDescriptionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
