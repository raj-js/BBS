import { NgModule } from '@angular/core';
import { ThemeModule } from '../../theme.module';
import { BoolRenderComponent } from './bool-render/bool-render.component';
import { DateRenderComponent } from './date-render/date-render.component';

const COMPONENTS = [
  BoolRenderComponent,
  DateRenderComponent
];

@NgModule({
  declarations: [...COMPONENTS],
  imports: [
    ThemeModule,
  ],
  entryComponents: [...COMPONENTS]
})
export class RendersModule { }
