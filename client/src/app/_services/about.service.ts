import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { About } from '../_models/about';
import { Feedback } from '../_models/feedback';

@Injectable({
  providedIn: 'root',
})
export class AboutService {
  baseUrl = environment.apiUrl;
  abouts: About[] = [];
  feedbacks: Feedback[] = [];

  constructor(private http: HttpClient) {}

  registerAbout(model: any) {
    return this.http.post(this.baseUrl + 'about', model).pipe(
      map((about: any) => {
        console.log(about);
        this.abouts.push(about);
      })
    );
  }

  getAbouts() {
    if (this.abouts.length > 0) return of(this.abouts);
    return this.http.get<About[]>(this.baseUrl + 'about').pipe(
      map((abouts) => {
        this.abouts = abouts;
        return abouts;
      })
    );
  }

  getFeedbacks() {
    if (this.feedbacks.length > 0) return of(this.feedbacks);
    return this.http.get<Feedback[]>(this.baseUrl + 'about/feed').pipe(
      map((feedbacks) => {
        this.feedbacks = feedbacks;
        return feedbacks;
      })
    );
  }

  updateAbout(about: About) {
    return this.http
      .put<About>(this.baseUrl + 'about/' + about.id, about)
      .pipe(map(() => {}));
  }

  updateFeedback(feedback: Feedback) {
    return this.http
      .put<Feedback>(this.baseUrl + 'about/feed/' + feedback.id, feedback)
      .pipe(map(() => {}));
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + 'about/delete-photo/' + photoId);
  }

  uploadAboutPhoto(file, aboutId: number): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post(
      this.baseUrl + 'about/add-photo/' + aboutId,
      formData
    );
  }

  deleteFeedPhoto(feedId: number) {
    return this.http.delete(this.baseUrl + 'about/feed/delete-photo/' + feedId);
  }

  uploadFeedPhoto(file, feedId: number): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);
    return this.http.post(
      this.baseUrl + 'about/feed/add-photo/' + feedId,
      formData
    );
  }

  registerFeedback(model: any) {
    return this.http.post<Feedback>(this.baseUrl + 'about/feed', model);
  }

  deleteFeedback(id: number) {
    return this.http.delete(this.baseUrl + 'about/feed/delete/' + id, {
      responseType: 'text',
    });
  }
}
