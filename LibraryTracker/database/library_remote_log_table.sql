CREATE TABLE IF NOT EXISTS `library_remote`.`library_log` (
  `id` INT NOT NULL AUTO_INCREMENT,
  `id_in_attendance_tracker` INT NOT NULL,
  `id_card_number` VARCHAR(200) NOT NULL,
  `crn` VARCHAR(150) NOT NULL,
  `date` DATETIME NOT NULL,
  `in_time` DATETIME NOT NULL,
  `out_time` DATETIME NULL,
  PRIMARY KEY (`id`),
  INDEX `id_card_number_IDX` (`id_card_number` ASC),
  UNIQUE INDEX `id_in_attendance_tracker_UNIQUE` (`id_in_attendance_tracker` ASC))
ENGINE = InnoDB;