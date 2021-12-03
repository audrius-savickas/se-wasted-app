import {configureStore} from "@reduxjs/toolkit"
import {reducer} from "../reducers"

export const createStore = (preloadedState?: Partial<ReturnType<typeof reducer>>) =>
  configureStore({
    reducer,
    preloadedState,
    middleware: getDefaultMiddleware => getDefaultMiddleware({serializableCheck: false})
  })

export const store = createStore()
