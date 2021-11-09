import React, {useEffect, useState} from "react"
import {StyleSheet} from "react-native"
import GetLocation, {Location} from "react-native-get-location"
import {Navigation} from "react-native-navigation"
import {Button, Colors, Incubator, LoaderScreen, RadioButton, RadioGroup, Text, View} from "react-native-ui-lib"
import {getAllRestaurants} from "../../../api"
import {Restaurant, RestaurantSortType} from "../../../api/interfaces"
import {RestaurantsList} from "../../../components/restaurants-list"
import {setHomeRoot} from "../../../services/navigation"
import {HOME_BUTTON} from "../home-button"
import {RestaurantListProps} from "./interfaces"

export const RestaurantList = ({componentId}: RestaurantListProps) => {
  const [location, setLocation] = useState({} as Location)
  const [restaurants, setRestaurants] = useState([] as Restaurant[])
  const [renderedRestaurants, setRenderedRestaurants] = useState([] as Restaurant[])
  const [loading, setLoading] = useState(true)
  const [searchValue, setSearchValue] = useState("")
  const [sortVisible, setSortVisible] = useState(false)
  const [sortType, setSortType] = useState(RestaurantSortType.DIST)

  const fetchLocation = async () => {
    setLocation(
      await GetLocation.getCurrentPosition({
        enableHighAccuracy: true,
        timeout: 15000
      })
    )
  }

  const fetchRestaurants = async (sortType: RestaurantSortType = RestaurantSortType.DIST) => {
    const response = await getAllRestaurants({
      sortType,
      coordinates: {longitude: location.longitude, latitude: location.latitude}
    })

    setRestaurants(response)
    setRenderedRestaurants(response)
    setLoading(false)
  }

  const search = () => {
    const filteredRestaurants = restaurants.filter(restaurant => {
      if (restaurant.name.toLowerCase().includes(searchValue.toLowerCase())) {
        return restaurant
      }
    })
    setRenderedRestaurants(filteredRestaurants)
  }

  const sort = async () => {
    setLoading(true)
    setRestaurants(
      await getAllRestaurants({
        sortType: sortType,
        coordinates: {longitude: location.longitude, latitude: location.latitude}
      })
    )
    setLoading(false)
  }

  useEffect(() => {
    fetchLocation()
    Navigation.mergeOptions(componentId, {topBar: {leftButtons: [HOME_BUTTON]}})
    const listener = Navigation.events().registerNavigationButtonPressedListener(({buttonId}) => {
      if (buttonId === "GO_BACK") {
        setHomeRoot()
      }
    })

    return () => listener.remove()
  }, [])

  useEffect(() => {
    if (location.longitude) {
      fetchRestaurants()
    }
  }, [location])

  useEffect(() => {
    if (restaurants.length) {
      setRenderedRestaurants(restaurants)
    }
  }, [restaurants])

  useEffect(() => {
    search()
  }, [searchValue])

  useEffect(() => {
    setSearchValue("")
    if (location.longitude) {
      sort()
    }
  }, [sortType])

  return (
    <View flex>
      <>
        <View marginH-s8 marginT-s4 marginB-s3>
          <View row br20 padding-s3style={{borderWidth: 1, borderColor: Colors.grey30}}>
            <View flexG>
              <Incubator.TextField
                text70
                autoCapitalize="none"
                label="Name"
                value={searchValue}
                labelStyle={{marginBottom: 4}}
                placeholder="Name of restaurant"
                fieldStyle={{borderBottomWidth: 1, borderColor: Colors.purple40, paddingBottom: 4}}
                onChangeText={setSearchValue}
              />
            </View>
            <Button
              bg-transparent
              marginR-s3
              iconStyle={{width: 21, height: 21}}
              iconSource={require("../../../../assets/control.png")}
              onPress={() => {
                setSortVisible(!sortVisible)
              }}
            />
          </View>
        </View>
        {loading ? (
          <LoaderScreen color={Colors.blue30} message="Loading..." />
        ) : (
          <RestaurantsList componentId={componentId} restaurants={renderedRestaurants} />
        )}
        {sortVisible && (
          <View bg-white br30 padding-s2 paddingH-s4 style={styles.filter}>
            <Text marginB-s2>Filters</Text>
            <RadioGroup collapsable initialValue={RestaurantSortType.DIST} onValueChange={setSortType}>
              <View marginV-s1>
                <RadioButton size={20} label="Distance" value={RestaurantSortType.DIST} />
              </View>
              <View marginV-s1>
                <RadioButton size={20} label="Name" value={RestaurantSortType.NAME} />
              </View>
            </RadioGroup>
          </View>
        )}
      </>
    </View>
  )
}

const styles = StyleSheet.create({
  filter: {
    position: "absolute",
    top: 60,
    right: 30,
    height: "auto",
    opacity: 100,
    borderColor: Colors.grey50,
    borderWidth: 1,
    shadowColor: Colors.black,
    shadowOpacity: 0.2,
    shadowOffset: {height: 0, width: 0}
  }
})
