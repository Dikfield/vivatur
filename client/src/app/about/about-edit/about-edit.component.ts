import {
  Component,
  HostListener,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { PromotionDescriptionComponent } from 'src/app/promotions/promotionDescription/promotionDescription.component';
import { About } from 'src/app/_models/about';
import { Feedback } from 'src/app/_models/feedback';
import { FeedbackPhoto } from 'src/app/_models/feedbackPhoto';
import { User } from 'src/app/_models/user';
import { VivaPhoto } from 'src/app/_models/vivaPhotos';
import { AboutService } from 'src/app/_services/about.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-about-edit',
  templateUrl: './about-edit.component.html',
  styleUrls: ['./about-edit.component.css'],
})
export class AboutEditComponent implements OnInit {
  abouts: About[] = [];
  feedbacks: Feedback[] = [];
  feedIndex: number = 0;
  Index: number = 0;
  photo: VivaPhoto;
  FeedPhoto: FeedbackPhoto;
  @ViewChild('dataForm') dataForm: NgForm;
  @ViewChild('feedForm') feedForm: NgForm;
  user: User;
  baseUrl = environment.apiUrl;
  shortLink: string = '';
  loading: boolean = false;
  file: File = null;
  modalRef?: BsModalRef;
  model: any = {};

  constructor(
    private aboutService: AboutService,
    private toastr: ToastrService,
    private modalService: BsModalService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadAbouts();
    this.loadFeedbacks();
  }

  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.dataForm.dirty) {
      $event.returnValue = true;
    }
  }

  loadAbouts() {
    this.aboutService.getAbouts().subscribe({
      next: (abouts) => {
        this.abouts = abouts;
      },
    });
  }

  loadFeedbacks() {
    this.aboutService.getFeedbacks().subscribe({
      next: (feedbacks) => {
        this.feedbacks = feedbacks;
      },
    });
  }
  updateAbout() {
    this.aboutService.updateAbout(this.abouts[this.Index]).subscribe({
      next: (response) => {
        this.toastr.success('Descrição atualizada');
        this.dataForm.reset(this.abouts[0]);
      },
    });
  }

  updateFeedback() {
    this.aboutService.updateFeedback(this.feedbacks[this.feedIndex]).subscribe({
      next: (response) => {
        this.toastr.success('Feedback atualizado');
        //this.feedForm.reset(feedback);
      },
      error(err) {
        console.log(err);
      },
    });
  }

  getIndex(index: number) {
    this.Index = index;
  }
  deletePhoto(photoId: number) {
    this.aboutService.deletePhoto(photoId).subscribe({
      next: () => {
        this.toastr.success;
        this.abouts[this.Index].vivaPhotos = this.abouts[
          this.Index
        ].vivaPhotos.filter((x) => x.id !== photoId);
      },
    });
  }

  onChange(event) {
    this.file = event.target.files[this.Index];
  }

  onUpload(aboutId: number) {
    this.loading = !this.loading;
    this.aboutService.uploadAboutPhoto(this.file, aboutId).subscribe({
      next: (event: any) => {
        if (typeof event === 'object') {
          this.loading = false;
          this.abouts[this.Index].vivaPhotos.push(event);
        }
      },
      error: (e) => console.log(e),
    });
  }

  deleteFeedPhoto(feedId: number) {
    this.aboutService.deleteFeedPhoto(feedId).subscribe({
      next: () => {
        this.toastr.success;
        this.feedbacks[this.feedIndex].feedbackPhoto = null;
      },
    });
  }

  onFeedUpload(feedId: number) {
    this.loading = !this.loading;
    this.aboutService.uploadFeedPhoto(this.file, feedId).subscribe({
      next: (event: any) => {
        if (typeof event === 'object') {
          this.loading = false;
          this.feedbacks[this.feedIndex].feedbackPhoto = event;
        }
      },
      error: (e) => console.log(e),
    });
  }

  registerFeed() {
    this.aboutService.registerFeedback(this.model).subscribe({
      next: (feedback: Feedback) => {
        this.feedbacks.push(feedback);
        this.toastr.success('Registered');
        this.closeModal();
      },
      error: (e) => {
        console.log(e);
        this.toastr.error(e.error);
      },
    });
  }

  feedbackDeleted(id: number) {
    this.aboutService.deleteFeedback(id).subscribe({
      next: () => {
        this.feedbacks = this.feedbacks.filter((feedback) => feedback.id != id);
        this.toastr.success('Deleted');
      },
    });
  }

  cancel() {
    console.log('cancelled');
  }

  forwardList() {
    if (this.feedIndex < this.feedbacks.length - 1) {
      this.feedIndex++;
    } else {
      this.feedIndex = 0;
    }
  }

  backwardList() {
    if (this.feedIndex > 0) {
      this.feedIndex--;
    } else {
      this.feedIndex = this.feedbacks.length - 1;
    }
  }

  openModal(template: TemplateRef<any>) {
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
