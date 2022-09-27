import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoPromotionEditorComponent } from './photo-promotion-editor.component';

describe('PhotoPromotionEditorComponent', () => {
  let component: PhotoPromotionEditorComponent;
  let fixture: ComponentFixture<PhotoPromotionEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotoPromotionEditorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhotoPromotionEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
