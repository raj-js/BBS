import { Component } from '@angular/core';
import { NbRequestPasswordComponent } from '@nebular/auth';

@Component({
  selector: 'ngx-forgot',
  templateUrl: './forgot.component.html',
})
export class ForgotComponent extends NbRequestPasswordComponent {
}
