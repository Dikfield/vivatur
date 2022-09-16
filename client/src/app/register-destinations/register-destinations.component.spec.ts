import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RegisterDestinationsComponent } from './register-destinations.component';

describe('RegisterDestinationsComponent', () => {
  let component: RegisterDestinationsComponent;
  let fixture: ComponentFixture<RegisterDestinationsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RegisterDestinationsComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(RegisterDestinationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
