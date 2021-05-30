import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { ServerStateComponent } from './server-state/server-state.component';
import { ServerInputComponent } from './server-input/server-input.component';

@NgModule({
  declarations: [
    AppComponent,
    ServerStateComponent,
    ServerInputComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
