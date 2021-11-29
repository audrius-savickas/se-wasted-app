import {State} from "../typings/redux"

export const getLocation = (state: State) => state.location.location

export const isLocationLoaded = (state: State) => state.location.locationLoaded

export const isLocationAllowed = (state: State) => state.location.locationAllowed
