import { NgForm } from '@angular/forms'
import { Component, OnInit } from '@angular/core';
import { ServerStateService } from '../server-state.service';

@Component({
  selector: 'app-server-input',
  templateUrl: './server-input.component.html',
  styleUrls: ['./server-input.component.css']
})
export class ServerInputComponent implements OnInit {

  constructor(private serverStateService: ServerStateService) { }

  ngOnInit(): void {
  }

  words: string = "";

  submitted = false;

  onSubmit(form: NgForm) {
    const splitted: string[] = this.words.split(/[, ]+/);
    this.submitted = true;
    this.serverStateService.postStartCommand(splitted);
    form.reset();
  }
}
