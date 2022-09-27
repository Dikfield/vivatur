import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterPromotionsComponent } from './register-promotions.component';

describe('RegisterPromotionsComponent', () => {
  let component: RegisterPromotionsComponent
  let fixture: ComponentFixture<RegisterPromotionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterPromotionsComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterPromotionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
