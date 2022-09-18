import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhotoDetinationEditorComponent } from './photo-detination-editor.component';

describe('PhotoDetinationEditorComponent', () => {
  let component: PhotoDetinationEditorComponent;
  let fixture: ComponentFixture<PhotoDetinationEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhotoDetinationEditorComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PhotoDetinationEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
