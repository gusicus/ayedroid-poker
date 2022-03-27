export interface TokenDto {
  token: string;
  expires: Date;
  refreshToken: string;
}

export interface UniqueEntity {
  id: string;
  name: string;
}

export interface SessionDto extends UniqueEntity {
  participants: ParticipantDto[];
  sizes: UniqueEntity[];
  topics: TopicDto[];
}

export interface ParticipantDto {
  userId: string;
  type: ParticipantType;
  name: string;
}

export interface TopicDto extends UniqueEntity {
  description: string;
  // UserId, Size
  votes: Map<string, UniqueEntity>;
}

export enum ParticipantType {
  None = 'None',
  Viewer = 'Viewer',
  Voter = 'Voter',
  Moderator = 'Moderator',
  Owner = 'Owner',
}
