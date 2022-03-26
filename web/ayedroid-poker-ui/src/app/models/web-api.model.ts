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
}

export interface ParticipantDto {
  userId: string;
  type: ParticipantType;
  name: string;
}

export enum ParticipantType {
  None = 0,
  Viewer = 1,
  Voter = 2,
  Moderator = 3,
  Owner = 4,
}
