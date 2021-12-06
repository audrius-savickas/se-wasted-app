import {useSelector} from "react-redux"
import {getUser} from "../selectors/authentication"

export const useAuthentication = () => {
  return {
    user: useSelector(getUser)
  }
}
