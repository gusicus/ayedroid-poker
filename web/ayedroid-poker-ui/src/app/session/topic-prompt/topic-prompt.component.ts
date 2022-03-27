import { Component } from '@angular/core';
import { TopicPromptResult } from './topic-prompt.model';

@Component({
  selector: 'pkr-topic-prompt',
  templateUrl: './topic-prompt.component.html',
  styleUrls: ['./topic-prompt.component.scss'],
})
export class TopicPromptComponent {
  public result: TopicPromptResult = {
    topic: '',
    description: '',
  };

  public constructor() {}
}
