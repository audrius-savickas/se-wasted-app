import React, {useEffect, useRef, useState} from "react"
import {ScrollView} from "react-native"
import {Navigation} from "react-native-navigation"
import {Colors, LoaderScreen, View} from "react-native-ui-lib"
import {CheapestFood} from "../../../components/sections/cheapest-food"
import {LatestFood} from "../../../components/sections/latest-food"
import {NearRestaurants} from "../../../components/sections/near-restaurants"
import {PopularRestaurants} from "../../../components/sections/popular-restaurants"
import {useLocation} from "../../../hooks/use-location"
import {HomeProps} from "./interfaces"

export const Home = ({componentId}: HomeProps) => {
  const [loading, setLoading] = useState(true)
  const [loaderMessage, setLoaderMessage] = useState("Loading...")
  const [sideMenuOpen, setSideMenuOpen] = useState(false)
  const {location, locationAllowed, locationLoaded, fetchLocation} = useLocation()
  const isMounted = useRef(false)

  useEffect(() => {
    if (locationLoaded) {
      if (locationAllowed) {
        setLoading(false)
      } else {
        setLoaderMessage("Please allow access to location to see restaurants")
      }
    }
  }, [locationLoaded, locationAllowed])

  useEffect(() => {
    fetchLocation()
    Navigation.mergeOptions(componentId, {
      sideMenu: {
        left: {
          visible: false,
          width: 260
        }
      }
    })
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "SIDE_MENU") {
        setSideMenuOpen(open => !open)
      }
    })
    return () => listener.remove()
  }, [])

  useEffect(() => {
    if (isMounted.current) {
      Navigation.mergeOptions(componentId, {
        sideMenu: {
          left: {
            visible: sideMenuOpen
          }
        }
      })
    } else {
      isMounted.current = true
    }
  }, [sideMenuOpen])

  return (
    <>
      {loading ? (
        <LoaderScreen color={Colors.blue30} message={loaderMessage} />
      ) : (
        <ScrollView>
          <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
            <PopularRestaurants componentId={componentId} location={location} />
          </View>
          <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
            <LatestFood componentId={componentId} />
          </View>
          <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
            <NearRestaurants componentId={componentId} location={location} />
          </View>
          <View style={{borderBottomColor: Colors.grey50, borderBottomWidth: 1}}>
            <CheapestFood componentId={componentId} />
          </View>
        </ScrollView>
      )}
    </>
  )
}
