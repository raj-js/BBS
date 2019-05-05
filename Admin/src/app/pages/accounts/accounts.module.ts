import { NgModule } from '@angular/core';
import { ThemeModule } from '../../@theme/theme.module';

import { AccountsComponent } from './accounts.component';
import { AccountListComponent } from './account-list/account-list.component';

import { AccountsRoutingModule } from './accounts-routing.module';
import { Ng2SmartTableModule } from 'ng2-smart-table';
import { RendersModule } from '../../@theme/components/renders/renders.module';

const COMPONENTS = [
  AccountsComponent,
  AccountListComponent,
];

@NgModule({
  declarations: [...COMPONENTS],
  imports: [
    ThemeModule,
    AccountsRoutingModule,
    Ng2SmartTableModule,
    RendersModule,
  ]
})
export class AccountsModule { }
