import { HttpClient, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

interface MailChimpResponse {
  result: string;
  msg: string;
}

@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.scss'],
})
export class FormComponent implements OnInit {
  form!: FormGroup;
  submitted = false;
  mailChimpEndpoint =
    'https://outlook.us21.list-manage.com/subscribe/post-json?u=865adb9487a7b29f43d302c44&amp;id=0c481e80a5&amp;f_id=00a9c0e1f0&';
  error = '';

  constructor(
    private toastr: ToastrService,
    private fb: FormBuilder,
    private http: HttpClient
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.form = this.fb.group({
      name: ['',Validators.required],
      email: ['', Validators.required],
      destino: ['', Validators.required],
      date: ['', Validators.required],
      nPessoas: ['', Validators.required],
      type: ['', Validators.required],
      inve: ['', Validators.required],
      tel: ['', Validators.required],
    });
  }

  submit() {
    console.log('ok');
    this.error = '';
    if (this.form.status === 'VALID') {
      const params = new HttpParams()
        .set('NAME', this.form.value.name)
        .set('TEL', this.form.value.tel)
        .set('EMAIL', this.form.value.email)
        .set('INVEST', this.form.value.inve)
        .set('TYPE', this.form.value.type)
        .set('PESSOAS', this.form.value.nPessoas)
        .set('DATES', this.form.value.date)
        .set('DEST', this.form.value.destino)
        .set('9ef819b47e6a071c039f2a12eea5af84-us21', '');

      const mailChimpUrl = this.mailChimpEndpoint + params.toString();

      this.http.jsonp<MailChimpResponse>(mailChimpUrl, 'c').subscribe(
        (response) => {
          if (response.result && response.result !== 'error') {
            this.submitted = true;
            this.toastr.success('Registrado com sucesso');
          } else {
            this.error = response.msg;
            this.toastr.error(this.error);
          }
        },
        (error) => {
          this.error = 'Sorry, an error occured.';
          this.toastr.error(error);
        }
      );
    } else {
      if (this.form.get('email')?.hasError('required')) {
        this.toastr.error('Email is required');
      } else {
        this.toastr.error('Preencha os demais campos');
      }
    }
  }
}
